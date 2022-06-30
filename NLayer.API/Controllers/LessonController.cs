using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NLayer.Core.DTOs;
using NLayer.Core.Models;
using NLayer.Core.Services;

namespace NLayer.API.Controllers
{
    public class LessonController : CustomBaseController
    {
        private readonly IMapper _mapper;

        private readonly IService<Lesson> _service;

        public LessonController(IService<Lesson> service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }
        [HttpGet]
        [Authorize(Roles = "Manager,Teacher")]
        public async Task<IActionResult> All()
        {
            var lessons = await _service.GetAllAsync();
            var lessonsDtos = _mapper.Map<List<LessonDto>>(lessons.ToList());
            return CreateActionResult(CustomResponseDto<List<LessonDto>>.Success(200, lessonsDtos));
        }
       

        [HttpGet("{id}")] //GET api/lessons/5  -- kullanıcıdan bir id bekliyoruz
        [Authorize(Roles = "Manager,Teacher")]
        public async Task<IActionResult> GeyById(int id)
        {
            var lessons = await _service.GetByIdAsync(id);
            var lessonDto = _mapper.Map<LessonDto>(lessons);
            return CreateActionResult(CustomResponseDto<LessonDto>.Success(200, lessonDto));
        }

        [HttpPost]
        [Authorize(Roles = "Manager,Teacher")]
        public async Task<IActionResult> Save(LessonDto lessonDto)
        {
            var lesson = await _service.AddAsync(_mapper.Map<Lesson>(lessonDto));
            var lessonsDto = _mapper.Map<LessonDto>(lesson);
            return CreateActionResult(CustomResponseDto<LessonDto>.Success(201, lessonsDto));
        }

        [HttpPut]
        [Authorize(Roles = "Manager,Teacher")]
        public async Task<IActionResult> Update(LessonDto lessonDto)
        {
            await _service.UpdateAsync(_mapper.Map<Lesson>(lessonDto)); //geriye bir şey dönmüyor
                                                                        //geriye bir şey dönmediği için NoContentDto classını dönüyorum
            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }

        [HttpDelete("{id}")] //DELETE api/products/5  -- kullanıcıdan bir id bekliyoruz
        [Authorize(Roles = "Manager,Teacher")]
        public async Task<IActionResult> Remove(int id)
        {
            var lesson = await _service.GetByIdAsync(id);
            await _service.RemoveAsync(lesson);
            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));
        }

    }
}
