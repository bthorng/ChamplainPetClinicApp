using System;
using System.Collections.Generic;
using System.Text;

namespace ChamplainPetClinicApp.Services.NetworkServices {
    public static class ApiConstants {

        private static readonly string HOST = "10.0.2.2";
        private static readonly string PORT = "80";

        public static string ApiGatewayUri() {
            return $"http://{HOST}:{PORT}/api/gateway";
        }
        
        public static string GetLoginUri() {
            return $"http://{HOST}:{PORT}/login";
        }

        public static string GetOwnersUri(int id) {
            if(id <= 0) {
                return $"{ApiGatewayUri()}/owners";
            }

            return $"{ApiGatewayUri()}/owners/{id}";
        }

        public static string GetVetsUri(int id) {
            if(id <= 0) {
                return $"{ApiGatewayUri()}/vets";
            }

            return $"{ApiGatewayUri()}/vets/{id}";
        }

        public static string GetVisitsForPetUri(string id, bool getScheduledVisits) {
            if(string.IsNullOrWhiteSpace(id)) throw new Exception("INVALID GET VISIT FOR PET ID");

            if(getScheduledVisits) {
                return $"{ApiGatewayUri()}/visits/scheduled/{id}";
            }

            return $"{ApiGatewayUri()}/visits/previous/{id}";
        }

        public static string DirectApiGatewayUri() {
            return $"http://{HOST}:{PORT}80/api/gateway";
        }

        // Couldn't get routes from Nariman, had to access API directly through 8080 for Post, Put & Delete a Visit
        public static string GetVisitByIdUri(string id) {
            return $"{DirectApiGatewayUri()}/visit/{id}";
        }

        public static string GetUpdateVisitUri(string petId, string visitId) {
            return $"{DirectApiGatewayUri()}/owners/*/pets/{petId}/visits/{visitId}";
        }

        public static string GetCreateVisitUri(string petId) {
            return $"{DirectApiGatewayUri()}/visit/owners/0/pets/{petId}/visits";
        }

        public static string GetDeleteVisitUri(string id) {
            return $"{DirectApiGatewayUri()}/visits/{id}";
        }

    }
}
