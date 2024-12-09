using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class ReviewsModel 
    {
        public int ReviewsId { get; set; }
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public string TextReviews { get; set; } = null!;
        public int? Rating { get; set; }
        public DateTime Rev1iewDate { get; set; }

    }
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        public modelsContext Context { get; }

        public ReviewController(modelsContext context)
        {
            Context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            List<Review> review = Context.Reviews.ToList();
            return Ok(review);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            Review? review = Context.Reviews.FirstOrDefault(x => x.ReviewsId == id);
            if (id <= 0)
            {
                return BadRequest("ID должен быть больше нуля.");
            }
            if (review == null)
            {
                return NotFound("Такого отзыва не существует");
            }
            return Ok(review);
        }

        [HttpPost]
        public IActionResult Add(ReviewsModel review)
        {


            var reviewAdd = new Review()
            {

                ReviewsId = review.ReviewsId,
                OrderId = review.OrderId,
                CustomerId = review.CustomerId,
                TextReviews = review.TextReviews,
                Rating = review.Rating,
                Rev1iewDate = review.Rev1iewDate,
            };


            Context.Reviews.Add(reviewAdd);
            Context.SaveChanges();
            return Ok(reviewAdd);

        }

        [HttpPut("{id}")]
        public IActionResult Update(ReviewsModel review,int id)
        {
            if (id <= 0)
            {
                return BadRequest("ID должен быть больше нуля.");
            }


            var reviewUpdate = new Review()
            {

                ReviewsId = review.ReviewsId,
                OrderId = review.OrderId,
                CustomerId = review.CustomerId,
                TextReviews = review.TextReviews,
                Rating = review.Rating,
                Rev1iewDate = review.Rev1iewDate,
            };


            Context.Reviews.Update(reviewUpdate);
            Context.SaveChanges();
            return Ok(reviewUpdate);

        }


        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Review? review = Context.Reviews.FirstOrDefault(x => x.ReviewsId == id);
            if (id <= 0)
            {
                return BadRequest("ID должен быть больше нуля.");
            }
            if (review == null)
            {
                return NotFound("Такого отзыва не существует");
            }
            Context.Reviews.Remove(review);
            Context.SaveChanges();
            return Ok(review);
        }
    }
}
