using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ChamplainPetClinicApp.Services.NetworkServices {
    public interface INetworkService<T> where T : HttpResponseMessage, new() {

        Task<List<TResult>> GetAsync<TResult>(string uri);
        Task<TResult> GetSingleAsync<TResult>(string uri);
        Task<bool> DeleteSingleAsync(string uri);
        Task<TResult> UpdateSingleAsync<TResult>(string uri, object data);
        Task<TResult> PostSingleAsync<TResult>(string uri, object data);
        Task<bool> LoginAsync(string uri, object user);
    }
}
