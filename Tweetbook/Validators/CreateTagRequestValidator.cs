using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tweetbook.Contracts.V1.Requests;

namespace Tweetbook.Validators
{
    public class CreateTagRequestValidator : AbstractValidator<CreatePostRequest>
    {
        public CreateTagRequestValidator()
        {
            RuleFor(q => q.Name)
                .NotEmpty()
                .Matches("^[a-zA-Z0-9 ]*$");
        }
    }
}
