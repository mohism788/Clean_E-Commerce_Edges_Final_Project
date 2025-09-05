using AutoMapper;
using Clean_E_Commerce_Project.API.DTOs.ReviewsDTOs;
using Clean_E_Commerce_Project.Core.Interfaces;
using Clean_E_Commerce_Project.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Clean_E_Commerce_Project.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ReviewsController(IUnitOfWork unitOfWork, IMapper mapper )
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllReviews()
        {
            var reviews = await _unitOfWork.ReviewsRepository.GetAllAsync();
            return Ok(reviews);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetReviewById(int id)
        {
            var review = await _unitOfWork.ReviewsRepository.GetByIdAsync(id);
            if (review == null)
            {
                return NotFound();
            }
            return Ok(review);
        }


        [HttpPost]
        public async Task<IActionResult> CreateReview([FromBody] CreateReviewDto newReview)
        {
            if (newReview == null)
            {
                return BadRequest();
            }
            var review = _mapper.Map<Review>(newReview);
            await _unitOfWork.ReviewsRepository.AddAsync(review);
            await _unitOfWork.SaveChangesAsync();
            return CreatedAtAction(nameof(GetReviewById), new { id = review.Id }, newReview);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateReview(int id, [FromBody] UpdatedReviewDto updatedReview)
        {
            if (updatedReview == null)
            {
                return BadRequest();
            }
            var existingReview = await _unitOfWork.ReviewsRepository.GetByIdAsync(id);
            if (existingReview == null)
            {
                return NotFound();
            }
            var newReview = _mapper.Map<Review>(updatedReview);
            existingReview.Rating = newReview.Rating;
            existingReview.Comment = newReview.Comment;
            existingReview.ProductId = newReview.ProductId;
            _unitOfWork.ReviewsRepository.Update(newReview);
            await _unitOfWork.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReview(int id)
        {
            var existingReview = await _unitOfWork.ReviewsRepository.GetByIdAsync(id);
            if (existingReview == null)
            {
                return NotFound();
            }
            _unitOfWork.ReviewsRepository.Delete(existingReview);
            await _unitOfWork.SaveChangesAsync();
            return NoContent();
        }


    }
}
