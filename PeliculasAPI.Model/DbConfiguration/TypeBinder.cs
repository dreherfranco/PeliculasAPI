using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PeliculasAPI.Model.DbConfiguration
{
    public class TypeBinder<T>: IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var nameProperty = bindingContext.ModelName;
            var valuesProvider = bindingContext.ValueProvider.GetValue(nameProperty);

            if(valuesProvider == ValueProviderResult.None)
            {
                return Task.CompletedTask;
            }

            try
            {
                var deserializedValue = JsonConvert.DeserializeObject<T>(valuesProvider.FirstValue);
                bindingContext.Result = ModelBindingResult.Success(deserializedValue);
            }
            catch
            {
                bindingContext.ModelState.TryAddModelError(nameProperty, $"invalid value for type {typeof(T)}");
            }
            return Task.CompletedTask;
        }
    }
}
