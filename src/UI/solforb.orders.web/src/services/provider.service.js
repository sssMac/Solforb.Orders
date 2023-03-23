import http from "../utils/http-helper";

class ProviderService{

    getProviders(){
        return http.get("/providers")
    }

    getProvider(id){
        return http.get(`/providers/${id}`)
    }
}

export  default new ProviderService();