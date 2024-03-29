﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tweetbook.Contracts.V1;
using Tweetbook.Contracts.V1.Requests;
using Tweetbook.Contracts.V1.Responses;
using Tweetbook.Services;

namespace Tweetbook.Controllers.V1
{
    public class IdentityController : Controller
    {
        private readonly IIdentityService identityService;

        public IdentityController(IIdentityService identityService)
        {
            this.identityService = identityService;
        }

        [HttpPost(ApiRoutes.Identity.Register)]
        public async Task<IActionResult> Register([FromBody]UserRegistrationRequest request)
        {
            if(!ModelState.IsValid)
            {
               return BadRequest(new AuthFailedResponse
               {
                   Errors = ModelState.Values.SelectMany(q => q.Errors.Select(a => a.ErrorMessage))
               });
            }

            var authResponse = await identityService.RegisterAsync(request.Email, request.Password);

            if (!authResponse.IsSuccess)
                return BadRequest(new AuthFailedResponse
                {
                    Errors = authResponse.Errors
                });

            return Ok(new AuthSuccessResponse {
                Token = authResponse.Token
            });
        }

        [HttpPost(ApiRoutes.Identity.Login)]
        public async Task<IActionResult> Login([FromBody]UserLoginRequest request)
        {
            var authResponse = await identityService.LoginAsync(request.Email, request.Password);

            if (!authResponse.IsSuccess)
                return BadRequest(new AuthFailedResponse
                {
                    Errors = authResponse.Errors
                });

            return Ok(new AuthSuccessResponse
            {
                Token = authResponse.Token,
                RefreshToken = authResponse.RefreshToken
            });
        }

        [HttpPost(ApiRoutes.Identity.Refresh)]
        public async Task<IActionResult> Login([FromBody]RefreshTokenRequest request)
        {
            var authResponse = await identityService.RefreshTokenAsync(request.Token, request.RefreshToken);

            if (!authResponse.IsSuccess)
                return BadRequest(new AuthFailedResponse
                {
                    Errors = authResponse.Errors
                });

            return Ok(new AuthSuccessResponse
            {
                Token = authResponse.Token,
                RefreshToken = authResponse.RefreshToken
            });
        }
    }
}