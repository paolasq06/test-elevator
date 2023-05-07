using Application.Auth.Commands;
using CleanArch.Application.Interfaces;
using Domain.Interfaces;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Courses.Commands
{
    public class PostLoginCommandValidator : AbstractValidator<PostLoginCommand>
    {
        private readonly IAuthRepository _authRepository;
        public PostLoginCommandValidator(
            IAuthRepository authRepository

            )
        {
            _authRepository = authRepository;
            
            RuleFor(c => c.Login).NotNull();
            RuleFor(c => c.Password).NotNull();
            //RuleFor(c => c.SubCampaignId)
            //    .NotNull();


        }


        
    }
}
