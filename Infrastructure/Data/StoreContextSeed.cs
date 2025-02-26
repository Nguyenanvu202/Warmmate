using System;
using System.Text.Json;
using Core.Entities;

namespace Infrastructure.Data;

public class StoreContextSeed
{
    public static async Task SeedAsync(StoreContext _storeContext){
        

        if(!_storeContext.ProductCategories.Any())
        {
            var productCategoryData = await File.ReadAllTextAsync("../Infrastructure/Data/SeedData/ProductCategory.json");
            var productCategory = JsonSerializer.Deserialize<List<ProductCategory>>(productCategoryData);

            if(productCategory == null) return ;
            _storeContext.ProductCategories.AddRange(productCategory);
            await _storeContext.SaveChangesAsync();
        }

        if(!_storeContext.ProductItems.Any())
        {
            var productItemsData = await File.ReadAllTextAsync("../Infrastructure/Data/SeedData/Items.json");
            var productItems = JsonSerializer.Deserialize<List<ProductItem>>(productItemsData);

            if(productItems == null) return ;
            _storeContext.ProductItems.AddRange(productItems);
            await _storeContext.SaveChangesAsync();
        }

        if(!_storeContext.ProductItemImgs.Any())
        {
            var itemImgsData = await File.ReadAllTextAsync("../Infrastructure/Data/SeedData/ItemImgs.json");
            var itemImgs = JsonSerializer.Deserialize<List<ProductItemImg>>(itemImgsData);

            if(itemImgs == null) return ;
            _storeContext.ProductItemImgs.AddRange(itemImgs);
            await _storeContext.SaveChangesAsync();
        }

        if(!_storeContext.Variations.Any())
        {
            var variationsData = await File.ReadAllTextAsync("../Infrastructure/Data/SeedData/Variation.json");
            var variations = JsonSerializer.Deserialize<List<Variation>>(variationsData);

            if(variations == null) return ;
            _storeContext.Variations.AddRange(variations);
            await _storeContext.SaveChangesAsync();
        }

        if(!_storeContext.VariationOpts.Any())
        {
            var variationOptsData = await File.ReadAllTextAsync("../Infrastructure/Data/SeedData/VariationOpt.json");
            var variationOpts = JsonSerializer.Deserialize<List<VariationOpt>>(variationOptsData);

            if(variationOpts == null) return ;
            _storeContext.VariationOpts.AddRange(variationOpts);
            await _storeContext.SaveChangesAsync();
        }

    }
}
