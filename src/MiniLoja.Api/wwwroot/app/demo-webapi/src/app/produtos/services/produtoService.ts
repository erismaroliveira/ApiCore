import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from "@angular/common/http";

import { Observable } from 'rxjs';
import { catchError, map } from "rxjs/operators";
import { throwError } from 'rxjs';
import { Fornecedor } from '../models/Fornecedor';
import { Produto } from '../models/Produto';

@Injectable()
export class ProdutoService {
    constructor(private http: HttpClient) { }

    protected UrlServiceV1: string = "https://localhost:5001/api/";

    obterTodos(): Observable<Produto[]> {
        return this.http
            .get<Produto[]>(this.UrlServiceV1 + "produtos")
            .pipe(
                catchError(this.serviceError));
    }

    registrarProdutoAlternativo(produto: FormData): Observable<Produto> {

        return this.http
            .post(this.UrlServiceV1 + 'produtos/adicionar', produto, this.ObterHeaderFormData())
            .pipe(
                map(this.extractData),
                catchError(this.serviceError)
            );
    }

    registrarProduto(produto: Produto): Observable<Produto> {

        return this.http
            .post(this.UrlServiceV1 + 'produtos', produto, this.ObterHeaderJson())
            .pipe(
                map(this.extractData),
                catchError(this.serviceError)
            );
    }

    obterFornecedores(): Observable<Fornecedor[]> {
        return this.http
            .get<Fornecedor[]>(this.UrlServiceV1 + 'fornecedores')
            .pipe(
                catchError(this.serviceError)
            );
    }

    protected ObterHeaderFormData() {
        return {
            headers: new HttpHeaders({
                'Content-Disposition': 'form-data; name="produto"'
            })
        };
    }

    protected ObterHeaderJson() {
        return {
            headers: new HttpHeaders({
                'Content-Type': 'application/json'
            })
        };
    }

    protected extractData(response: any) {
        return response.data || {};
    }

    protected serviceError(error: Response | any) {
        let errMsg: string;

        if (error instanceof Response) {

            errMsg = `${error.status} - ${error.statusText || ''}`;
        }
        else {
            errMsg = error.message ? error.message : error.toString();
        }

        console.error(error);
        return throwError(error);
    }

}