import type { ProblemDetails } from "./ProblemDetails";

export class ApiResponce<T> {
    public responce: T | null;
    public error: ProblemDetails | null;
    public status: number;

    constructor(responce: T | null, error: ProblemDetails | null, status: number ) {
        this.responce = responce,
        this.error = error,
        this.status = status
    }

    public get IsSucces(): boolean {
        return this.status >= 200 && this.status < 300;
    }
}