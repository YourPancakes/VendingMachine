using AutoMapper;
using FluentResults;
using VendingMachine.Application.DTOs.Cart;
using VendingMachine.Application.Services;
using VendingMachine.Domain.Entities;
using VendingMachine.Infrastructure.Persistence.Repositories.Interfaces;

namespace VendingMachine.Application.Services.Implementations;

public class CartService : ICartService
{
    private readonly ICartRepository _cartRepository;
    private readonly IProductRepository _productRepository;
    private readonly ICoinRepository _coinRepository;
    private readonly ICoinService _coinService;
    private readonly IMapper _mapper;
    private readonly ILogger<CartService> _logger;

    public CartService(ICartRepository cartRepository, IProductRepository productRepository, 
        ICoinRepository coinRepository, ICoinService coinService, IMapper mapper, ILogger<CartService> logger)
    {
        _cartRepository = cartRepository;
        _productRepository = productRepository;
        _coinRepository = coinRepository;
        _coinService = coinService;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<Result<CartDto>> GetCartAsync()
    {
        var cart = await _cartRepository.GetCartAsync();
        if (cart == null)
        {
            _logger.LogWarning("Cart not found, creating new cart");
            cart = await _cartRepository.CreateCartAsync();
        }

        var cartDto = _mapper.Map<CartDto>(cart);
        return Result.Ok(cartDto);
    }

    public async Task<Result<CartDto>> AddToCartAsync(AddToCartDto addToCartDto)
    {
        _logger.LogWarning("Adding product to cart: ProductId={ProductId}, Quantity={Quantity}", 
            addToCartDto.ProductId, addToCartDto.Quantity);
        
        var product = await _productRepository.GetByIdAsync(addToCartDto.ProductId);
        if (product == null)
        {
            _logger.LogWarning("Product not found: ProductId={ProductId}", addToCartDto.ProductId);
            return Result.Fail<CartDto>("Product not found");
        }

        if (product.Quantity < addToCartDto.Quantity)
        {
            _logger.LogWarning("Insufficient stock: Requested={Requested}, Available={Available}", 
                addToCartDto.Quantity, product.Quantity);
            return Result.Fail<CartDto>($"Insufficient stock. Available: {product.Quantity}");
        }

        var cart = await _cartRepository.GetCartAsync();
        if (cart == null)
        {
            _logger.LogWarning("Cart not found, creating new cart");
            cart = await _cartRepository.CreateCartAsync();
        }

        var existingCartItems = await _cartRepository.GetCartItemsByProductIdAsync(addToCartDto.ProductId);
        var existingCartItemWithSamePrice = existingCartItems.FirstOrDefault(ci => ci.UnitPrice == product.Price);
        var existingCartItemWithOldPrice = existingCartItems.FirstOrDefault(ci => ci.UnitPrice != product.Price);

        if (existingCartItemWithSamePrice != null)
        {
            var newQuantity = existingCartItemWithSamePrice.Quantity + addToCartDto.Quantity;
            // No need to log quantity changes in detail
            
            if (newQuantity <= 0)
            {
                // no log
                await _cartRepository.RemoveCartItemByIdAsync(existingCartItemWithSamePrice.Id);
            }
            else
            {
                existingCartItemWithSamePrice.Quantity = newQuantity;
                await _cartRepository.UpdateCartItemAsync(existingCartItemWithSamePrice);
            }
        }
        else if (existingCartItemWithOldPrice != null)
        {
            _logger.LogWarning("Product price changed. Old price: {OldPrice}, New price: {NewPrice}", 
                existingCartItemWithOldPrice.UnitPrice, product.Price);
            return Result.Fail<CartDto>($"Product price has changed. Current price: {product.Price:C}. Cannot add at old price.");
        }
        else
        {
            if (addToCartDto.Quantity <= 0)
            {
                // no log
            }
            else
            {
                // no log
                var newCartItem = new CartItem
                {
                    CartId = cart.Id,
                    ProductId = product.Id,
                    ProductName = product.Name,
                    BrandName = product.Brand.Name,
                    UnitPrice = product.Price,
                    Quantity = addToCartDto.Quantity
                };

                await _cartRepository.AddCartItemAsync(newCartItem);
            }
        }

        cart.UpdatedAt = DateTime.UtcNow;
        await _cartRepository.SaveChangesAsync();

        var updatedCart = await _cartRepository.GetCartAsync();
        var cartDto = _mapper.Map<CartDto>(updatedCart);
        return Result.Ok(cartDto);
    }

    public async Task<Result<CartDto>> UpdateCartItemAsync(UpdateCartItemDto updateCartItemDto)
    {
        var existingCartItem = await _cartRepository.GetCartItemByIdAsync(updateCartItemDto.CartItemId);
        if (existingCartItem == null)
        {
            return Result.Fail<CartDto>("Item not found in cart");
        }

        var product = await _productRepository.GetByIdAsync(existingCartItem.ProductId);
        if (product == null)
        {
            return Result.Fail<CartDto>("Product not found");
        }

        if (updateCartItemDto.Quantity <= 0)
        {
            return await RemoveCartItemByIdAsync(updateCartItemDto.CartItemId);
        }

        if (existingCartItem.UnitPrice != product.Price && updateCartItemDto.Quantity > existingCartItem.Quantity)
        {
            return Result.Fail<CartDto>($"Product price has changed. Current price: {product.Price:C}. Cannot increase quantity at old price.");
        }

        if (product.Quantity < updateCartItemDto.Quantity)
        {
            return Result.Fail<CartDto>($"Insufficient stock. Available: {product.Quantity}");
        }

        existingCartItem.Quantity = updateCartItemDto.Quantity;

        await _cartRepository.UpdateCartItemAsync(existingCartItem);

        var cart = await _cartRepository.GetCartAsync();
        cart!.UpdatedAt = DateTime.UtcNow;
        await _cartRepository.SaveChangesAsync();

        var cartDto = _mapper.Map<CartDto>(cart);
        return Result.Ok(cartDto);
    }

    public async Task<Result<CartDto>> RemoveCartItemByIdAsync(int cartItemId)
    {
        // no log
        
        var existingCartItem = await _cartRepository.GetCartItemByIdAsync(cartItemId);
        if (existingCartItem == null)
        {
            _logger.LogWarning("Cart item not found: CartItemId={CartItemId}", cartItemId);
            return Result.Fail<CartDto>("Cart item not found");
        }

        // no log

        await _cartRepository.RemoveCartItemByIdAsync(cartItemId);

        var cart = await _cartRepository.GetCartAsync();
        if (cart != null)
        {
            cart.UpdatedAt = DateTime.UtcNow;
            await _cartRepository.SaveChangesAsync();
        }

        var cartDto = _mapper.Map<CartDto>(cart);
        return Result.Ok(cartDto);
    }

    public async Task<Result> ClearCartAsync()
    {
        await _cartRepository.ClearCartAsync();
        return Result.Ok();
    }

    public async Task<Result<decimal>> GetCartTotalAsync()
    {
        // no log
        var cart = await _cartRepository.GetCartAsync();
        if (cart == null)
        {
            // no log
            return Result.Ok(0m);
        }
        // no log
        return Result.Ok(cart.Total);
    }

    public async Task<Result<PurchaseResultDto>> ProcessPurchaseAsync()
    {
        // no log
        
        var cart = await _cartRepository.GetCartAsync();
        if (cart == null || !cart.CartItems.Any())
        {
            _logger.LogWarning("Cart is empty or null");
            return Result.Fail<PurchaseResultDto>("Cart is empty");
        }

        var changeCalculation = await _coinService.CalculateDetailedChangeAsync(cart.Total);
        if (changeCalculation.IsFailed)
        {
            _logger.LogError("Failed to calculate change: {Errors}", string.Join(", ", changeCalculation.Errors.Select(e => e.Message)));
            return Result.Fail<PurchaseResultDto>(string.Join("; ", changeCalculation.Errors.Select(e => e.Message)));
        }

        if (!changeCalculation.Value.CanPurchase)
        {
            _logger.LogWarning("Cannot process purchase: {Message}", changeCalculation.Value.Message);
            return Result.Fail<PurchaseResultDto>(changeCalculation.Value.Message);
        }

        foreach (var cartItem in cart.CartItems)
        {
            var product = await _productRepository.GetByIdAsync(cartItem.ProductId);
            if (product == null)
            {
                _logger.LogError("Product not found: ProductId={ProductId}", cartItem.ProductId);
                return Result.Fail<PurchaseResultDto>($"Product not found: {cartItem.ProductName}");
            }

            if (product.Quantity < cartItem.Quantity)
            {
                _logger.LogError("Insufficient stock for product: {ProductName}, Requested: {Requested}, Available: {Available}", 
                    product.Name, cartItem.Quantity, product.Quantity);
                return Result.Fail<PurchaseResultDto>($"Insufficient stock for {product.Name}. Available: {product.Quantity}");
            }

            product.Quantity -= cartItem.Quantity;
            await _productRepository.UpdateAsync(product);
            // no log
        }

        var coins = await _coinRepository.GetAllAsync();
        foreach (var coin in coins)
        {
            if (coin.Quantity > 0)
            {
                coin.Quantity = 0;
                await _coinRepository.UpdateAsync(coin);
                // no log
            }
        }

        var purchaseResult = new PurchaseResultDto
        {
            Success = true,
            Message = "Purchase completed successfully",
            TotalPaid = cart.Total,
            ChangeAmount = changeCalculation.Value.ChangeAmount,
            ChangeCoins = changeCalculation.Value.ChangeCoins,
            PurchasedItems = _mapper.Map<List<CartItemDto>>(cart.CartItems),
            PurchaseTime = DateTime.UtcNow
        };

        await _cartRepository.ClearCartAsync();
        // no log

        // no log

        return Result.Ok(purchaseResult);
    }
} 