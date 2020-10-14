using FluentValidation;
using Library.Core.Requests;

namespace Library.Core.Validators
{
    /// <summary>
    /// Defines validator for image request.
    /// Required fields: Image
    /// </summary>
    public class ImageRequestValidator : AbstractValidator<ImageRequest>
    {
        public ImageRequestValidator()
        {
            RuleFor(r => r.Image)
                 .Cascade(CascadeMode.Stop)
                 .NotNull().WithMessage("Image is required.");
            // TODO: check image type valid .Must(i => i.ContentType.Equals(MediaTypeNames.Image.Jpeg));
        }
    }
}
