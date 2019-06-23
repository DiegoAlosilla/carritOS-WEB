﻿using carritOSCore.Model.Context;
using carritOSCore.Model.Entities;
using carritOSCore.Model.Service;
using carritOSCore.Model.ServiceImpl;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

/**
 * --
 * @author Juan Diego Alosilla
 * @email diegoalosillagmail.com
 */
namespace carritOSCore.Controllers
{
    [Produces("application/json")]
    [Route("api/BusinessOwner")]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class BusinessOwnerController : Controller
    {
        private readonly ApplicationDbContext context;
        private IBusinessOwnerService BusinessOwnerService;
        public BusinessOwnerController(ApplicationDbContext context)
        {
            this.context = context;
            BusinessOwnerService = new BusinessOwnerServiceImpl(context);
        }

        [HttpGet]
        public IEnumerable<BusinessOwner> Get()
        {
            var BusinessOwner = BusinessOwnerService.FindAll();
            return BusinessOwner.ToList();
        }

        [HttpGet("{id}", Name = "create BusinessOwner")]
        public IActionResult GetById(int id)
        {

            var BusinessOwner = BusinessOwnerService.FindById(id);

            if (BusinessOwner == null)
            {
                return NotFound();
            }

            return Ok(BusinessOwner);
        }

        [HttpPost]
        public IActionResult Post([FromBody] BusinessOwner BusinessOwner)
        {
            if (ModelState.IsValid)
            {
                BusinessOwnerService.Save(BusinessOwner);
                return new CreatedAtRouteResult("create BusinessOwner", new { id = BusinessOwner.Id });
            }
            return BadRequest(ModelState);
        }


        [HttpPut("{id}")]
        public IActionResult Put([FromBody] BusinessOwner BusinessOwner, int id)
        {
            if (BusinessOwner.Id != id)
            {
                return BadRequest();
            }

            BusinessOwnerService.Update(BusinessOwner);
            return Ok();
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var BusinessOwner = BusinessOwnerService.FindById(id);

            if (BusinessOwner == null)
            {
                return NotFound();
            }

            BusinessOwnerService.Delete(BusinessOwner);
            return Ok(BusinessOwner);
        }

    }
}

