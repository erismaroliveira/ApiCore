import { __decorate, __metadata } from "tslib";
import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from "@angular/common/http";
import { catchError, map } from "rxjs/operators";
import { throwError } from 'rxjs';
var ProdutoService = /** @class */ (function () {
    function ProdutoService(http) {
        this.http = http;
        this.UrlServiceV1 = "https://localhost:5001/api/";
    }
    ProdutoService.prototype.obterTodos = function () {
        return this.http
            .get(this.UrlServiceV1 + "produtos")
            .pipe(catchError(this.serviceError));
    };
    ProdutoService.prototype.registrarProdutoAlternativo = function (produto) {
        return this.http
            .post(this.UrlServiceV1 + 'produtos/adicionar', produto, this.ObterHeaderFormData())
            .pipe(map(this.extractData), catchError(this.serviceError));
    };
    ProdutoService.prototype.registrarProduto = function (produto) {
        return this.http
            .post(this.UrlServiceV1 + 'produtos', produto, this.ObterHeaderJson())
            .pipe(map(this.extractData), catchError(this.serviceError));
    };
    ProdutoService.prototype.obterFornecedores = function () {
        return this.http
            .get(this.UrlServiceV1 + 'fornecedores')
            .pipe(catchError(this.serviceError));
    };
    ProdutoService.prototype.ObterHeaderFormData = function () {
        return {
            headers: new HttpHeaders({
                'Content-Disposition': 'form-data; name="produto"'
            })
        };
    };
    ProdutoService.prototype.ObterHeaderJson = function () {
        return {
            headers: new HttpHeaders({
                'Content-Type': 'application/json'
            })
        };
    };
    ProdutoService.prototype.extractData = function (response) {
        return response.data || {};
    };
    ProdutoService.prototype.serviceError = function (error) {
        var errMsg;
        if (error instanceof Response) {
            errMsg = error.status + " - " + (error.statusText || '');
        }
        else {
            errMsg = error.message ? error.message : error.toString();
        }
        console.error(error);
        return throwError(error);
    };
    ProdutoService = __decorate([
        Injectable(),
        __metadata("design:paramtypes", [HttpClient])
    ], ProdutoService);
    return ProdutoService;
}());
export { ProdutoService };
//# sourceMappingURL=produtoService.js.map