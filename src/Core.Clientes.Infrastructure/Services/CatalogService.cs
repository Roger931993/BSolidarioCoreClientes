using Core.Clientes.Application.DTOs.Base;
using Core.Clientes.Application.DTOs.Catalog;
using Core.Clientes.Application.Interfaces.Infraestructure;
using Core.Clientes.Domain.Common;
using Core.Clientes.Infrastructure.Services.Common;
using Core.Clientes.Infrastructure.Services.Common.Api;
using Core.Clientes.Shared.Helpers;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;
using static Core.Clientes.Model.Entity.EnumTypes;

namespace Core.Clientes.Infrastructure.Services
{
    public class CatalogService : BaseService, ICatalogService
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMemoryCacheLocalService _memoryCacheLocalService;
        private readonly IApiUrl _apiUrl;
        public CatalogService(HttpClient httpClient, IHttpContextAccessor httpContextAccessor, IMemoryCacheLocalService memoryCacheLocalService, ApiConnectionDto apiConnectionDto)
        {
            this._httpClient = httpClient;
            this._httpContextAccessor = httpContextAccessor;
            this._memoryCacheLocalService = memoryCacheLocalService;
            this._apiUrl = apiConnectionDto.Values!["Catalog"];
        }

        public async Task<ResponseBase<GetErrorByIdResponse>> GetCatalogErrorById(int id, Guid IdTraker)
        {
            DataCacheLocal cachelocal = await _memoryCacheLocalService.GetCachedData(IdTraker.ToString());
            try
            {
                var token = _httpContextAccessor.HttpContext?.Request.Headers["Authorization"].ToString();
                if (!string.IsNullOrWhiteSpace(token))
                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Replace("Bearer ", ""));          

                var response = await _httpClient.GetAsync($"{_apiUrl.Url}/code-error/{id}");               
                ResponseBase<GetErrorByIdResponse> resultService = await Util.ConvertResponse<ResponseBase<GetErrorByIdResponse>>(response);                     
                return resultService!;
            }
            catch (Exception ex)
            {
                await AddLogError(id.ToString(), 500, ex, cachelocal);
                return new ResponseBase<GetErrorByIdResponse>
                {
                    error = new Error
                    {
                        codeError = (int)TypeError.InternalError,
                        messageError = ex.Message,
                        success = false
                    }
                };
            }
            finally
            {
                await AddLogInput(id.ToString(), 200, cachelocal);
            }
        }
    }
}
