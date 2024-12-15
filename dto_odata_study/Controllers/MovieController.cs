using dto_odata_study.DTOs.dto_odata_study.DTOs;
using dto_odata_study.Models;
using dto_odata_study.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

[Route("odata/[controller]")]
public class MoviesController : ODataController
{
    private readonly IGeneralService<MovieModel, MovieDto> _service;

    public MoviesController(IGeneralService<MovieModel, MovieDto> service)
    {
        _service = service;
    }

    [EnableQuery]
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var movies = await _service.GetAllAsync();
        return Ok(movies.AsQueryable());
    }

    [EnableQuery]
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var movie = await _service.GetByIdAsync(id);
        if (movie == null) return NotFound();
        return Ok(movie);
    }
}