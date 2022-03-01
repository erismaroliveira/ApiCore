using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace MiniLoja.Api.Extensions
{
    public class MvcJsonOptions : IEnumerable<ICompatibilitySwitch>, IEnumerable
    {
        public JsonConverter[] SerializerSettings { get; internal set; }

        IEnumerator<ICompatibilitySwitch> IEnumerable<ICompatibilitySwitch>.GetEnumerator()
        {
            throw null;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw null;
        }

        public MvcJsonOptions()
        {
            throw new Exception("error");
        }
    }

    public class JsonWithFilesFormDataModelBinder : IModelBinder
    {
        private readonly IOptions<MvcJsonOptions> _jsonOptions;
        private readonly FormFileModelBinder _formFileModelBinder;

        public JsonWithFilesFormDataModelBinder(IOptions<MvcJsonOptions> jsonOptions, ILoggerFactory loggerFactory)
        {
            _jsonOptions = jsonOptions;
            _formFileModelBinder = new FormFileModelBinder(loggerFactory);
        }

        public async Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
                throw new ArgumentNullException(nameof(bindingContext));

            // Retrieve the form part containing the JSON
            var valueResult = bindingContext.ValueProvider.GetValue(bindingContext.FieldName);
            if (valueResult == ValueProviderResult.None)
            {
                // The JSON was not found
                var message = bindingContext.ModelMetadata.ModelBindingMessageProvider.MissingBindRequiredValueAccessor(bindingContext.FieldName);
                bindingContext.ModelState.TryAddModelError(bindingContext.ModelName, message);
                return;
            }

            var rawValue = valueResult.FirstValue;

            // Deserialize the JSON
            var model = JsonConvert.DeserializeObject(rawValue, bindingContext.ModelType, _jsonOptions.Value.SerializerSettings);

            // Now, bind each of the IFormFile properties from the other form parts
            foreach (var property in bindingContext.ModelMetadata.Properties)
            {
                if (property.ModelType != typeof(IFormFile))
                    continue;

                var fieldName = property.BinderModelName ?? property.PropertyName;
                var modelName = fieldName;
                var propertyModel = property.PropertyGetter(bindingContext.Model);
                ModelBindingResult propertyResult;
                using (bindingContext.EnterNestedScope(property, fieldName, modelName, propertyModel))
                {
                    await _formFileModelBinder.BindModelAsync(bindingContext);
                    propertyResult = bindingContext.Result;
                }

                if (propertyResult.IsModelSet)
                {
                    // The IFormFile was sucessfully bound, assign it to the corresponding property of the model
                    property.PropertySetter(model, propertyResult.Model);
                }
                else if (property.IsBindingRequired)
                {
                    var message = property.ModelBindingMessageProvider.MissingBindRequiredValueAccessor(fieldName);
                    bindingContext.ModelState.TryAddModelError(modelName, message);
                }
            }

            // Set the successfully constructed model as the result of the model binding
            bindingContext.Result = ModelBindingResult.Success(model);
        }
    }
}
#if false // Decompilation log
'307' items in cache
------------------
Resolve: 'System.Runtime, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Runtime, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Load from: 'C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\5.0.0\ref\net5.0\System.Runtime.dll'
------------------
Resolve: 'Microsoft.AspNetCore.Mvc.Core, Version=5.0.0.0, Culture=neutral, PublicKeyToken=adb9793829ddae60'
Found single assembly: 'Microsoft.AspNetCore.Mvc.Core, Version=5.0.0.0, Culture=neutral, PublicKeyToken=adb9793829ddae60'
Load from: 'C:\Program Files\dotnet\packs\Microsoft.AspNetCore.App.Ref\5.0.0\ref\net5.0\Microsoft.AspNetCore.Mvc.Core.dll'
------------------
Resolve: 'Microsoft.Extensions.DependencyInjection.Abstractions, Version=5.0.0.0, Culture=neutral, PublicKeyToken=adb9793829ddae60'
Found single assembly: 'Microsoft.Extensions.DependencyInjection.Abstractions, Version=6.0.0.0, Culture=neutral, PublicKeyToken=adb9793829ddae60'
WARN: Version mismatch. Expected: '5.0.0.0', Got: '6.0.0.0'
Load from: 'C:\Users\erism\.nuget\packages\microsoft.extensions.dependencyinjection.abstractions\6.0.0\lib\netstandard2.1\Microsoft.Extensions.DependencyInjection.Abstractions.dll'
------------------
Resolve: 'Microsoft.Extensions.Options, Version=5.0.0.0, Culture=neutral, PublicKeyToken=adb9793829ddae60'
Found single assembly: 'Microsoft.Extensions.Options, Version=6.0.0.0, Culture=neutral, PublicKeyToken=adb9793829ddae60'
WARN: Version mismatch. Expected: '5.0.0.0', Got: '6.0.0.0'
Load from: 'C:\Users\erism\.nuget\packages\microsoft.extensions.options\6.0.0\lib\netstandard2.1\Microsoft.Extensions.Options.dll'
------------------
Resolve: 'Microsoft.Extensions.Logging.Abstractions, Version=5.0.0.0, Culture=neutral, PublicKeyToken=adb9793829ddae60'
Found single assembly: 'Microsoft.Extensions.Logging.Abstractions, Version=5.0.0.0, Culture=neutral, PublicKeyToken=adb9793829ddae60'
Load from: 'C:\Program Files\dotnet\packs\Microsoft.AspNetCore.App.Ref\5.0.0\ref\net5.0\Microsoft.Extensions.Logging.Abstractions.dll'
------------------
Resolve: 'Microsoft.AspNetCore.Mvc.Abstractions, Version=5.0.0.0, Culture=neutral, PublicKeyToken=adb9793829ddae60'
Found single assembly: 'Microsoft.AspNetCore.Mvc.Abstractions, Version=5.0.0.0, Culture=neutral, PublicKeyToken=adb9793829ddae60'
Load from: 'C:\Program Files\dotnet\packs\Microsoft.AspNetCore.App.Ref\5.0.0\ref\net5.0\Microsoft.AspNetCore.Mvc.Abstractions.dll'
------------------
Resolve: 'System.Runtime.Serialization.Xml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Runtime.Serialization.Xml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Load from: 'C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\5.0.0\ref\net5.0\System.Runtime.Serialization.Xml.dll'
------------------
Resolve: 'System.Xml.ReaderWriter, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Xml.ReaderWriter, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Load from: 'C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\5.0.0\ref\net5.0\System.Xml.ReaderWriter.dll'
------------------
Resolve: 'Microsoft.AspNetCore.Http.Abstractions, Version=5.0.0.0, Culture=neutral, PublicKeyToken=adb9793829ddae60'
Found single assembly: 'Microsoft.AspNetCore.Http.Abstractions, Version=5.0.0.0, Culture=neutral, PublicKeyToken=adb9793829ddae60'
Load from: 'C:\Program Files\dotnet\packs\Microsoft.AspNetCore.App.Ref\5.0.0\ref\net5.0\Microsoft.AspNetCore.Http.Abstractions.dll'
------------------
Resolve: 'Microsoft.AspNetCore.WebUtilities, Version=5.0.0.0, Culture=neutral, PublicKeyToken=adb9793829ddae60'
Found single assembly: 'Microsoft.AspNetCore.WebUtilities, Version=5.0.0.0, Culture=neutral, PublicKeyToken=adb9793829ddae60'
Load from: 'C:\Program Files\dotnet\packs\Microsoft.AspNetCore.App.Ref\5.0.0\ref\net5.0\Microsoft.AspNetCore.WebUtilities.dll'
------------------
Resolve: 'System.Xml.XmlSerializer, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Xml.XmlSerializer, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Load from: 'C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\5.0.0\ref\net5.0\System.Xml.XmlSerializer.dll'
------------------
Resolve: 'System.Runtime.Serialization.Primitives, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Found single assembly: 'System.Runtime.Serialization.Primitives, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a'
Load from: 'C:\Program Files\dotnet\packs\Microsoft.NETCore.App.Ref\5.0.0\ref\net5.0\System.Runtime.Serialization.Primitives.dll'
#endif
