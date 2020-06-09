using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimpleShop.API.Data;
using SimpleShop.API.Dtos;
using SimpleShop.API.Models;

namespace SimpleShop.API.Controllers
{
    [Authorize]
    [Route("api/users/{userId}/purchase")]
    [ApiController]
    public class PurchaseController : ControllerBase
    {
        private readonly IShoppingRepository _repository;
        private readonly IMapper _mapper;
        private readonly DataContext _context;
        public PurchaseController(IShoppingRepository repository, IMapper mapper, DataContext context)
        {
            _context = context;
            _mapper = mapper;
            _repository = repository;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPurchaseByPurchaseId(int id, int userId)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value)) return Unauthorized();
            var purchaseFromRepo = await _repository.GetPurchase(id);
            if (purchaseFromRepo != null)
            {
            return Ok(purchaseFromRepo);
            }
            return BadRequest();
        }

        [HttpGet]
        public async Task<IActionResult> GetPurchases(int userId)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value)) return Unauthorized();
            var purchases = await _context.Purchases.AsQueryable()
                .Where(u => u.UserId == userId).ToListAsync();
            return Ok(purchases);
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> MakePurchase(int userId, int id)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value)) return Unauthorized();
            var user = await _repository.GetUser(userId);
            if ( await _context.Articles.FirstOrDefaultAsync(x => x.Id == id) != null)
            {
                var article = await _repository.GetArticle(id);
                var purchase = new Purchase();
                purchase.ArticleId = article.Id;
                purchase.UserId = user.Id;
                await _context.Purchases.AddAsync(purchase);
                if (await _repository.saveAll()) return Ok();
            }
            return BadRequest("Failed to add purchase");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> UndoPurchase(int userId, int id)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value)) return Unauthorized();
            var user = await _repository.GetUser(userId);
            var purchaseFromRepo = await _repository.GetPurchase(id);
            if (purchaseFromRepo != null)
            {
                if (purchaseFromRepo.UserId != userId) return Unauthorized();
                _repository.Delete(purchaseFromRepo);
                if (await _repository.saveAll()) return Ok();
            }
            return BadRequest("Failed to delete the purchase");
        }
    }
}