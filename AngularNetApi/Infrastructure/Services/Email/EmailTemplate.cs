using RazorLight;

namespace AngularNetApi.Infrastructure.Services.Email
{
    public class EmailTemplate
    {
        private readonly IRazorLightEngine _razorLightEngine;

        public EmailTemplate()
        {
            var templatePath = Path.Combine(
                Directory.GetCurrentDirectory(),
                "Infrastructure/Services/Email/Templates"
            );
            _razorLightEngine = new RazorLightEngineBuilder()
                .UseFileSystemProject(templatePath) // Passa solo il percorso della radice dei template
                .UseMemoryCachingProvider()
                .Build();
        }

        public async Task<string> RenderTemplateAsync<T>(string templatePath, T model)
        {
            // Assicurati che il templatePath sia solo il nome del file, non il percorso completo
            return await _razorLightEngine.CompileRenderAsync(templatePath, model);
        }
    }
}
