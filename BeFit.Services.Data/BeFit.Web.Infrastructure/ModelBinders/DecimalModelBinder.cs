using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Globalization;

namespace BeFit.Web.Infrastructure.ModelBinders
{
	public class DecimalModelBinder : IModelBinder
	{
		public Task BindModelAsync(ModelBindingContext bindingContext)
		{
			if (bindingContext == null)
			{
				throw new ArgumentNullException(nameof(bindingContext));
			}

			ValueProviderResult valueResult = 
				bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
			if (valueResult != ValueProviderResult.None && !string.IsNullOrWhiteSpace(valueResult.FirstValue))
			{
				double parsedValue = 0;
				bool binderSucceeded = false;

				try
				{
					string formDecValue = valueResult.FirstValue;
					formDecValue = formDecValue.Replace(",", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator);
					formDecValue = formDecValue.Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator);

					parsedValue = Convert.ToDouble(formDecValue);
					binderSucceeded = true;
				}
				catch (FormatException fe)
				{
					bindingContext.ModelState.AddModelError(bindingContext.ModelName, fe, bindingContext.ModelMetadata);
				}

				if (binderSucceeded)
				{
					bindingContext.Result = ModelBindingResult.Success(parsedValue);
				}
			}

			return Task.CompletedTask;
		}
	}
}
