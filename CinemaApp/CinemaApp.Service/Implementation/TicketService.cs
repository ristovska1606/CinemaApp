using EShop.Domain.DomainModels;
using EShop.Domain.DTO;
using EShop.Domain.Relationship;
using EShop.Repository.Interface;
using EShop.Service.Interface;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EShop.Service.Implementation
{
    public class ProductService : IProductService
    {
        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<ProductInShoppingCart> _productInShoppingCartRepository;
        private readonly IUserRepository _userRepository;
        private readonly ILogger<ProductService> _logger;

        public ProductService(IRepository<Product> productRepository,ILogger<ProductService> logger, IRepository<ProductInShoppingCart> productInShoppingCartRepository, IUserRepository userRepository)
        {
            _productRepository = productRepository;
            _productInShoppingCartRepository = productInShoppingCartRepository;
            _userRepository = userRepository;
            _logger = logger;
        }

        public Boolean AddProductInShoppingCart(AddToShoppingCardDto entity, string userId)
        {
            var product = this.GetSpecificProduct(entity.SelectedProductId);

            var loggedInUser = _userRepository.Get(userId);

            var UserCart = loggedInUser.UserShoppingCart;

            if (product != null && loggedInUser != null && UserCart != null)
            {
                var itemToAdd = new ProductInShoppingCart
                {
                    Id = Guid.NewGuid(),
                    ShoppingCart = UserCart,
                    Product = product,
                    ProductId = product.Id,
                    ShoppingCartId = UserCart.Id,
                    Quantity = entity.Quantity
                };

                _productInShoppingCartRepository.Insert(itemToAdd);

                _logger.LogInformation("Product was successfully added into ShoppingCart.");
                return true;
            }else
            {
                _logger.LogInformation("Something was wrong. ProductId or UserShoppingCart are not available.");
                return false;
            }
            
        }

        public Product CreateNewProduct(Product newEntity)
        {
            newEntity.Id = Guid.NewGuid();
            return _productRepository.Insert(newEntity);
        }

        public Product DeleteProduct(Guid? id)
        {
            var poductToDelete = this.GetSpecificProduct(id);
            return _productRepository.Delete(poductToDelete);
        }

        public AddToShoppingCardDto GetAddToShoppingCartDto(Guid? id)
        {
            var selectedProduct = this.GetSpecificProduct(id);


            var model = new AddToShoppingCardDto
            {
                SelectedProduct = selectedProduct,
                SelectedProductId = selectedProduct.Id,
                Quantity = 1
            };

            return model;
        }

        public List<Product> GetAllProductAsList()
        {
            _logger.LogInformation("GetAllProductAsList was called.");
            return _productRepository.GetAll().ToList();
        }

        public Product GetSpecificProduct(Guid? id)
        {
            return _productRepository.Get(id);
        }

        public bool ProductExist(Guid? id)
        {
            var productExist = this.GetSpecificProduct(id);

            return productExist != null;
        }

        public Product UpdateExistingProduct(Product updatedProduct)
        {
            return _productRepository.Update(updatedProduct);
        }
    }
}
