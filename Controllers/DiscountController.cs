using System.ComponentModel;
using System.Reflection.Emit;
using System.Net.Mail;
using System.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using DiscountCodes.DTOs;
using DiscountCodes.Models;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace DiscountCodes.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DiscountController : ControllerBase
    {
        private readonly ILogger<DiscountController> _logger;
        private readonly IMapper _mapper;
        private static IConfiguration _configuration;
        // private static readonly List<DiscountCode> _petsInMemoryStore = new List<DiscountCode>();
        private readonly DiscountDB _discountDB;

        public DiscountController(ILogger<DiscountController> logger, DiscountDB discountDB)
        {
            _logger = logger;
            _discountDB = discountDB;
        }


        [HttpGet("{id:Guid}/{userId:Guid}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<DiscountCode> GetById(Guid id, Guid userId)
        {
            var code = _discountDB.DiscountCodes.FirstOrDefault(p => p.Id == id);

            if (code == null) {
                return NotFound();
            }
            var hasBeenUsed = _discountDB.DiscountUses.Any(x => x.DiscountCodeId == code.Id && x.UserId == userId);

            Console.WriteLine("#####"+ hasBeenUsed);

            if (hasBeenUsed) {
                return NotFound();
            }

            var discountUse = new DiscountUse() {
                    Id = Guid.NewGuid(),
                    Created = DateTime.Now,
                    UserId = userId,
                    DiscountCodeId = id
                };

            _discountDB.DiscountUses.Add(discountUse);
            _discountDB.SaveChanges();

            return code;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<DiscountCodeDTO> Create(DiscountCodeCreateDTO code)
        {
            List<string> failed = new List<string>();
            List<string> created = new List<string>();

            foreach (var item in code.Codes)
            {
                var exists = _discountDB.DiscountCodes.Any(x => x.Code == item);

                if (exists) {
                    failed.Add(item);
                } else {
                    created.Add(item);
                }

                var discountCode = new DiscountCode() {
                    Id = Guid.NewGuid(),
                    Code = item,
                    Created = DateTime.Now,
                    StoreId = code.StoreId
                };

                _discountDB.DiscountCodes.Add(discountCode);
            }

            _discountDB.SaveChanges();

            code.CodesCreated = created;
            code.CodesFailed = failed;

            return Created("/", code);
        }
    }
}
