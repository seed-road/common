using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace SeedRoad.Common.Presentation.WebApi.Multipart;

public class JsonModelBinder : IModelBinder
{
    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
        if (bindingContext == null)
        {
            throw new ArgumentNullException(nameof(bindingContext));
        }

        var valueProviderResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
        if (valueProviderResult == ValueProviderResult.None) return Task.CompletedTask;
        bindingContext.ModelState.SetModelValue(bindingContext.ModelName, valueProviderResult);

        var valueAsString = valueProviderResult.FirstValue;
        if (valueAsString is null) return Task.CompletedTask;

        var result = Newtonsoft.Json.JsonConvert.DeserializeObject(valueAsString, bindingContext.ModelType);

        if (result == null) return Task.CompletedTask;
        bindingContext.Result = ModelBindingResult.Success(result);
        return Task.CompletedTask;
    }
}