using ChamplainPetClinicApp.Models;
using ChamplainPetClinicApp.Views;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ChamplainPetClinicApp.Services.NetworkServices {
    public sealed class NetworkService : INetworkService<HttpResponseMessage> {

        private static readonly Lazy<NetworkService> lazy =
        new Lazy<NetworkService>(() => new NetworkService());

        public static NetworkService Instance { get { return lazy.Value; } }

        private NetworkService() {
            httpClient = new HttpClient();
        }

        private static HttpClient httpClient = null;

        public async Task<bool> LoginAsync(string uri, object user) {
            try {
                var json = JsonConvert.SerializeObject(user);
                var jsonString = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await httpClient.PostAsync(uri, jsonString);
                
                if(response.IsSuccessStatusCode) {
                    HttpHeaders headers = response.Headers;
                    
                    if(headers.TryGetValues("Authorization", out IEnumerable<string> values)) {
                        var JWTToken = values.First();

                        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", JWTToken);

                        return true;
                    }
                }

                return false;
            } catch(TaskCanceledException) {
                return false;
            }
        }

        public async Task<bool> DeleteSingleAsync(string uri) {
            try {
                HttpResponseMessage response = await httpClient.DeleteAsync(uri);

                if(response.StatusCode == HttpStatusCode.Forbidden) {
                    await Application.Current.MainPage.DisplayAlert("Login expired!", "Please log in again!", "Return to login");
                    await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
                }

                return response.IsSuccessStatusCode;
            } catch(TaskCanceledException) {
                return false;
            }
        }

        public async Task<List<TResult>> GetAsync<TResult>(string uri) {
            try {
                HttpResponseMessage response = await httpClient.GetAsync(uri);

                if(response.StatusCode == HttpStatusCode.Forbidden) {
                    await Application.Current.MainPage.DisplayAlert("Login expired!", "Please log in again!", "Return to login");
                    await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
                }

                if(response.IsSuccessStatusCode) {
                    string serialized = await response.Content.ReadAsStringAsync();

                    var result = JsonConvert.DeserializeObject<List<TResult>>(serialized);

                    return result;
                }

                return default(List<TResult>);
            } catch(TaskCanceledException) {
                return default(List<TResult>);
            }
        }

        public async Task<TResult> GetSingleAsync<TResult>(string uri) {
            try {
                HttpResponseMessage response = await httpClient.GetAsync(uri);

                if(response.StatusCode == HttpStatusCode.Forbidden) {
                    await Application.Current.MainPage.DisplayAlert("Login expired!", "Please log in again!", "Return to login");
                    await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
                }

                if(response.IsSuccessStatusCode) {
                    string serialized = await response.Content.ReadAsStringAsync();

                    var result = JsonConvert.DeserializeObject<TResult>(serialized);

                    return result;
                }

                return default(TResult);
            } catch(TaskCanceledException) {
                return default(TResult);
            }
        }

        public async Task<TResult> UpdateSingleAsync<TResult>(string uri, object data) {
            var json = JsonConvert.SerializeObject(data);
            var jsonString = new StringContent(json, Encoding.UTF8, "application/json");

            try {
                HttpResponseMessage response = await httpClient.PutAsync(uri, jsonString);

                if(response.StatusCode == HttpStatusCode.Forbidden) {
                    await Application.Current.MainPage.DisplayAlert("Login expired!", "Please log in again!", "Return to login");
                    await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
                }

                if(response.IsSuccessStatusCode) {
                    string serialized = await response.Content.ReadAsStringAsync();

                    var result = JsonConvert.DeserializeObject<TResult>(serialized);

                    return result;
                }

                return default(TResult);
            } catch(TaskCanceledException) {
                return default(TResult);
            }
        }

        public async Task<TResult> PostSingleAsync<TResult>(string uri, object data) {
            var json = JsonConvert.SerializeObject(data);
            var jsonString = new StringContent(json, Encoding.UTF8, "application/json");

            try {
                HttpResponseMessage response = await httpClient.PostAsync(uri, jsonString);

                if(response.StatusCode == HttpStatusCode.Forbidden) {
                    await Application.Current.MainPage.DisplayAlert("Login expired!", "Please log in again!", "Return to login");
                    await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
                }

                if(response.IsSuccessStatusCode) {
                    string serialized = await response.Content.ReadAsStringAsync();

                    var result = JsonConvert.DeserializeObject<TResult>(serialized);

                    return result;
                }

                return default(TResult);
            } catch(TaskCanceledException) {
                return default(TResult);
            }
        }
    }
}
