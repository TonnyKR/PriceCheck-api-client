using Amazon.Translate.Model;
using Amazon.Translate;
using AutoMapper;
using PriceCheck.BusinessLogic.Dtos;
using PriceCheck.BusinessLogic.Dtos.ATB;
using PriceCheck.BusinessLogic.Interfaces;
using PriceCheck.Data.Entities;
using PriceCheck.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;



namespace PriceCheck.BusinessLogic.Services
{
    public class ATBService : IATBService
    {
        private readonly IShopRepository _repository;
        private readonly IMapper _mapper;
        private readonly ITransliterationService _transliterationService;
        private readonly IConfiguration _configuration;
        public ATBService(IShopRepository repository, IMapper mapper, ITransliterationService transliterationService, IConfiguration configuration)
        {
            _repository = repository;
            _mapper = mapper;
            _transliterationService = transliterationService;
            _configuration = configuration;
        }
        public async Task<IShopDto> CreateShopProduct(IShopDto shopDto)
        {
            var product = _mapper.Map<ATB>(shopDto);
            _repository.Add(product);
            await _repository.SaveChangesAsync();
            var productDto = _mapper.Map<ATBDto>(product);
            return productDto;

        }

        public async Task DeleteShopProduct(int id)
        {
            await _repository.Delete<ATB>(id);
            await _repository.SaveChangesAsync();
        }

        public async Task<IEnumerable<IShopDto>> GetAllShopProducts()
        {
            var productList = await _repository.GetAll<ATB>();
            var productDtoList = _mapper.Map<List<ATBDto>>(productList);
            return productDtoList;
        }

        public async Task<IShopDto> GetShopProduct(int id)
        {
            var product = await _repository.GetById<ATB>(id);
            var productDto = _mapper.Map<ATBDto>(product);
            return productDto;
        }

        public async Task<IShopDto> GetShopProductByLink(string link)
        {
            var product = await _repository.GetByLink<ATB>(link);
            var productDto = _mapper.Map<ATBDto>(product);
            return productDto;
        }

        public async Task<IEnumerable<IShopDto>> GetShopProductsByName(string name)
        {
            var products = await _repository.GetByName<ATB>(name);
            var productsDto = _mapper.Map<IEnumerable<ATBDto>>(products);
            return productsDto;
        }
        public async Task<IEnumerable<IShopDto>> GetShopProductsByFuzzyName(string name)
        {
            name = name.First().ToString().ToUpper() + string.Join("", name.Skip(1));
            IEnumerable<ATB>? translatedProducts = null;

            if (!Regex.IsMatch(name, "^[a-zA-Z0-9]*$"))
            {
                var accessKey = _configuration.GetValue<string>("AWS:AccessKey");
                var secretKey = _configuration.GetValue<string>("AWS:SecretKey");
                var translateClient = new AmazonTranslateClient(accessKey, secretKey, Amazon.RegionEndpoint.EUNorth1);

                TranslateTextRequest translateTextRequest = new TranslateTextRequest() { SourceLanguageCode = "uk", TargetLanguageCode = "en", Text = name.Trim() };
                var tranlateResponse = await translateClient.TranslateTextAsync(translateTextRequest);

                await Console.Out.WriteLineAsync(tranlateResponse.TranslatedText);
                translatedProducts = await _repository.GetByNameFuzzy<ATB>(tranlateResponse.TranslatedText);
                
            }
            var products = await _repository.GetByNameFuzzy<ATB>(name);
            products = (products ?? Enumerable.Empty<ATB>()).Concat(translatedProducts ?? Enumerable.Empty<ATB>());


            var productsDto = _mapper.Map<IEnumerable<ATBDto>>(products);
            return productsDto;
        }

        public async Task UpdateShopProduct(int? id, IShopUpdateDto shopUpdateDto)
        {
            var product = await _repository.GetById<ATB>(id);
            product = _mapper.Map(shopUpdateDto, product);
            await _repository.SaveChangesAsync();
        }

    }
}
