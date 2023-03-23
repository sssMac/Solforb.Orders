import http from "../utils/http-helper";
import createDateAsUTC from "../utils/date-helper";

class OrderService{

    getOrders(filters){

        const params = new URLSearchParams()
        Object.keys(filters).forEach((key) => {
            if (filters[key])
                params.append(key, filters[key])
        })

        console.log(params)
        return http.get("/orders", {params}, )
    }

    getOrder(id){
        return http.get(`/orders/${id}`)
    }

    createOrder(data){
        return http.post("/orders", data)
    }

    editOrder(id, data){
        return http.put(`/orders/${id}`, data)
    }

    deleteOrder(id){
        return http.delete(`/orders/${id}`)
    }
}

export default new OrderService();