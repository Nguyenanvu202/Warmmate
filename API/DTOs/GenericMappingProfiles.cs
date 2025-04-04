using System;
using AutoMapper;
using Core.Entities;
using Core.Entities.OrderAggregate;

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

        //Map AppUser to RegisterDTO
        CreateMap<RegisterDTO, AppUser>()
        .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email)).ReverseMap();
        
        CreateMap<AddressDTO, Address>();
        CreateMap<Address, AddressDTO>();

        CreateMap<OrderItem, OrderItemDTO>()
        .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ItemOrdered.ProductId))
        .ForMember(dest => dest.ProductName,opt => opt.MapFrom(src => src.ItemOrdered.ProductName))
        .ForMember(dest => dest.PictureUrl,opt => opt.MapFrom(src => src.ItemOrdered.PictureUrl)).ReverseMap();


        CreateMap<Order, OrderDTO>()
        .ForMember(dest => dest.DeliveryMethod, opt => opt.MapFrom(src => src.DeliveryMethod.Description))
        .ForMember(dest => dest.ShippingPrice, opt => opt.MapFrom(src => src.DeliveryMethod.Price))
        .ForMember(dest => dest.Total, opt => opt.MapFrom(src => src.GetTotal()))
        .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
        .ForMember(dest => dest.OrderItems, opt => opt.MapFrom(src => src.OrderItems)).ReverseMap();
        
    }

   
    }

