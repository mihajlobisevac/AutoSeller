using Application.Common.Mappings;
using Application.V1.Brands.Commands;
using Application.V1.Brands.Queries;
using Application.V1.Models.Commands;
using Application.V1.Models.Queries;
using Application.V1.Posts.Commands;
using Application.V1.Posts.Queries;
using AutoMapper;
using Domain.Entities;
using System;
using System.Runtime.Serialization;
using Xunit;

namespace Application.UnitTests.Common.Mappings
{
    public class MappingTests
    {
        private readonly IConfigurationProvider _configuration;
        private readonly IMapper _mapper;

        public MappingTests()
        {
            _configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            _mapper = _configuration.CreateMapper();
        }

        [Fact]
        public void ShouldHaveValidConfiguration()
        {
            _configuration.AssertConfigurationIsValid();
        }

        [Theory]
        [InlineData(typeof(Brand), typeof(CreateBrand.Response))]
        [InlineData(typeof(Brand), typeof(EditBrand.Response))]
        [InlineData(typeof(Brand), typeof(GetBrands.Response))]
        [InlineData(typeof(Brand), typeof(GetBrandByName.Response))]
        [InlineData(typeof(Model), typeof(GetBrandByName.ModelDto))]
        [InlineData(typeof(Model), typeof(CreateModel.Response))]
        [InlineData(typeof(Model), typeof(EditModel.Response))]
        [InlineData(typeof(Model), typeof(GetModelsByBrandId.Response))]
        [InlineData(typeof(Model), typeof(GetModelsByBrandName.Response))]
        [InlineData(typeof(Post), typeof(EditPost.Response))]
        [InlineData(typeof(Post), typeof(GetPost.Response))]
        [InlineData(typeof(Post), typeof(GetPosts.Response))]
        public void ShouldSupportMappingFromSourceToDestination(Type source, Type destination)
        {
            var instance = GetInstanceOf(source);

            _mapper.Map(instance, source, destination);
        }

        private static object GetInstanceOf(Type type)
        {
            if (type.GetConstructor(Type.EmptyTypes) != null)
                return Activator.CreateInstance(type);

            // Type without parameterless constructor
            return FormatterServices.GetUninitializedObject(type);
        }
    }
}
