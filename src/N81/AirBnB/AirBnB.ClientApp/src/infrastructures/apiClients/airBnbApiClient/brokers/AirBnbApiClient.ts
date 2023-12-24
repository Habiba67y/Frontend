import ApiClientBase from "../../apiClientBase/ApiClientBase";
import { LocationEndpointsClient } from "./LocationEndpointsClient";

export class AirBnBApiClientBase {
    private readonly client: ApiClientBase;
    public readonly baseUrl: string;

    constructor() {
        this.baseUrl = "";

        this.client = new ApiClientBase({
            baseURL: this.baseUrl
        })

        this.locations = new LocationEndpointsClient(this.client);

    }

    public locations: LocationEndpointsClient;
}