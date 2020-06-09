using System;
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
    [Route("api/[controller]")]
    [ApiController]
    public class ArticleController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IShoppingRepository _repository;
        private readonly DataContext _context;
        public ArticleController(IShoppingRepository repository, IMapper mapper, DataContext context)
        {
            _context = context;
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetArticle(int id)
        {
            var article = await _repository.GetArticle(id);
            return Ok(article);
        }

        [HttpGet]
        public async Task<IActionResult> GetArticles()
        {
            var articles = await _context.Articles.ToListAsync();
            return Ok(articles);
        }

        [HttpPost]
        public async Task<IActionResult> AddArticle(ArticleForCreationDto articleForCreation)
        {
            var article = new Article();
            article.Name = articleForCreation.Name;
            article.Price = articleForCreation.Price;
            await _context.Articles.AddAsync(article);
            if(await _repository.saveAll()) return Ok();
            return BadRequest("Failed to add article");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateArticle(int id, ArticleForCreationDto articleForCreation)
        {
            var articleFromRepo = await _repository.GetArticle(id);
            _mapper.Map(articleForCreation, articleFromRepo);
            if (await _repository.saveAll()) return NoContent();
            throw new Exception($"Updating article with {id} failed on save.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteArticle(int id)
        {
            var articleFromRepo = await _repository.GetArticle(id);
            if (articleFromRepo != null)
            {
                _repository.Delete(articleFromRepo);
                if (await _repository.saveAll()) return Ok();
            }
            return BadRequest("Failed to delete the article");
        }
    }
}