using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;

namespace Pardisan.Extensions
{
    public static class SwaggerExtention
    {
        public static IServiceCollection AddSwaggerDoc(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Project API Documents",
                    Description = "Project API Swagger Documentation ",
                    TermsOfService = new Uri("https://Project.ir"),
                    Contact = new OpenApiContact
                    {
                        Name = "Project Develpoers",
                        Email = "dev@Project.ir",
                        Url = new Uri("https://Project.ir"),
                    },
                });

                c.UseAllOfToExtendReferenceSchemas();

                //c.AddEnumsWithValuesFixFilters(services, o =>
                //{
                //    o.ApplySchemaFilter = true;

                //    o.XEnumNamesAlias = "x-enum-varnames";

                //    o.XEnumDescriptionsAlias = "x-enum-descriptions";

                //    o.ApplyParameterFilter = true;

                //    o.ApplyDocumentFilter = true;

                //    o.IncludeDescriptions = true;

                //    o.IncludeXEnumRemarks = true;

                //    o.DescriptionSource = DescriptionSources.DescriptionAttributesThenXmlComments;
                //});

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Description = @"JWT Authorization header using the Bearer scheme. 
                      Enter your token in the text input below.
                      Example: '12345abcdef'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,
                        },
                        new List<string>()
                    }
                });

            });

            return services;
        }
    }
}