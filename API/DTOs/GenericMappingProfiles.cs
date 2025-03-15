using System;
using AutoMapper;
using Core.Entities;

namespace API.DTOs;

public class GenericMappingProfiles:Profile
{
    public GenericMappingProfiles(){

        // Map ProductItem to ItemDTO
        CreateMap<ProductItem, ItemDTO>()
            .ForMember(dest => dest.ProductItemImgs, opt => opt.MapFrom(src => src.ProductItemImgs)) // Map nested collection
            .ForMember(dest => dest.VariationOpts, opt => opt.MapFrom(src => src.VariationOpts)); // Map nested collection

        CreateMap<ItemDTO, ProductItem>()
            .ForMember(dest => dest.ProductItemImgs, opt => opt.MapFrom(src => src.ProductItemImgs)) // Map nested collection
            .ForMember(dest => dest.VariationOpts, opt => opt.MapFrom(src => src.VariationOpts)); // Map nested collection

        // Map ProductItemImg to ImgDTO and vice versa
        CreateMap<ProductItemImg, ImgDTO>().ReverseMap();

        // Map VariationOpt to OptDTO and vice versa
        CreateMap<VariationOpt, OptDTO>()
            .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Value)); // Map the Value property

        // Map OptDTO to VariationOpt
        CreateMap<OptDTO, VariationOpt>()
            .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Value)) // Map the Value property
            .ForMember(dest => dest.ProductItems, opt => opt.Ignore()); // Ignore the ProductItems property

        // Map Variation to VariationDTO
        CreateMap<Variation, VariationDTO>().ReverseMap();
    }

   
    }

