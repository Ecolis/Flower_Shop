using Microsoft.AspNetCore.Mvc;
using Flower_Shop.Models;
using Flower_Shop.Services;

namespace Flower_Shop.Controllers
{
    [ApiController]
    [Route("api/flowers")]
    public class Flowers : ControllerBase
    {
        private readonly IFlowerRepository _repo;

        public Flowers(IFlowerRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_repo.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var flower = _repo.GetById(id);

            if (flower == null)
            {
                return NotFound($"Цветок с ID {id} не найден");
            }

            return Ok(flower);
        }
    }
}