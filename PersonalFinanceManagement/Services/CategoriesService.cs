﻿using CsvHelper;
using PersonalFinanceManagement.Data;
using PersonalFinanceManagement.Dto;
using PersonalFinanceManagement.Factories;
using PersonalFinanceManagement.Models;
using PersonalFinanceManagement.Services;
using PersonalFinanceManagement.Services.Interfaces;
using System.Globalization;


    public class CategoriesService : ICategoryInterface
    {
        private PFMDbContext Context;

        public CategoriesService(PFMDbContext context)
        {
            Context = context;
        }

        public async Task<CategoryDto> GetCategoryById(int id)
        {
            CategoriesModel category = await Context.Categories.FindAsync(id);
            if (category != null)
            {
                return CategoryFactory.ToDto(category);
            }
            else return null;
        }

        public async Task<List<CategoriesModel>> ImportCategoriesAsync(IFormFile csv)
        {
            using (var streamReader = new StreamReader(csv.OpenReadStream()))
            {
                using (var csvReader = new CsvReader(streamReader, CultureInfo.InvariantCulture))
                {
                    csvReader.Context.RegisterClassMap<CategoriesMapper>();
                var categories = csvReader.GetRecords<CategoriesModel>();

                foreach (var category in categories)
                {
                    var DBCategory = Context.Categories.Find(category.Code);
                    if (DBCategory == null) {
                        Context.Categories.Add(category);
                        await Context.SaveChangesAsync();
                    } else
                    {
                        DBCategory.ParentCode = category.ParentCode;
                        DBCategory.Name = category.Name;
                        await Context.SaveChangesAsync();
                    }               
                }
                    return categories.ToList();
                }
            }
            return new List<CategoriesModel>();
        }
    }
