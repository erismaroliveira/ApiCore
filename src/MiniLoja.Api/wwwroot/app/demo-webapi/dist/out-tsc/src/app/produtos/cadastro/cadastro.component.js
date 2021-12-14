import { __decorate, __metadata } from "tslib";
import { Component } from '@angular/core';
import { FormBuilder, FormControl } from '@angular/forms';
import { Router } from '@angular/router';
import { ProdutoService } from '../services/produtoService';
var CadastroComponent = /** @class */ (function () {
    function CadastroComponent(fb, router, produtoService) {
        var _this = this;
        this.fb = fb;
        this.router = router;
        this.produtoService = produtoService;
        this.errors = [];
        this.produtoService.obterFornecedores()
            .subscribe(function (fornecedores) { return _this.fornecedores = fornecedores; }, function (fail) { return _this.errors = fail.error.errors; });
        this.imagemForm = new FormData();
    }
    CadastroComponent.prototype.ngOnInit = function () {
        this.produtoForm = this.fb.group({
            fornecedorId: '',
            nome: '',
            descricao: '',
            imagemUpload: '',
            imagem: '',
            valor: '0',
            ativo: new FormControl(false),
            nomeFornecedor: ''
        });
    };
    CadastroComponent.prototype.cadastrarProduto = function () {
        var _this = this;
        if (this.produtoForm.valid && this.produtoForm.dirty) {
            var produtoForm = Object.assign({}, this.produto, this.produtoForm.value);
            produtoForm.ativo = this.produtoForm.get('ativo').value;
            this.produtoHandle(produtoForm)
                .subscribe(function (result) { _this.onSaveComplete(result); }, function (fail) { _this.onError(fail); });
        }
    };
    CadastroComponent.prototype.onSaveComplete = function (response) {
        this.router.navigate(['/lista-produtos']);
    };
    CadastroComponent.prototype.onError = function (fail) {
        this.errors = fail.error.errors;
    };
    CadastroComponent.prototype.produtoHandleAlternativo = function (produto) {
        var formdata = new FormData();
        produto.imagem = this.imagemNome;
        produto.imagemUpload = null;
        formdata.append('produto', JSON.stringify(produto));
        formdata.append('ImagemUpload', this.imagemForm, this.imagemNome);
        return this.produtoService.registrarProdutoAlternativo(formdata);
    };
    CadastroComponent.prototype.produtoHandle = function (produto) {
        produto.imagemUpload = this.imageBase64;
        produto.imagem = this.imagemNome;
        return this.produtoService.registrarProduto(produto);
    };
    CadastroComponent.prototype.upload = function (file) {
        // necessario para upload via IformFile
        this.imagemForm = file[0];
        this.imagemNome = file[0].name;
        // necessario para upload via base64
        var reader = new FileReader();
        reader.onload = this.manipularReader.bind(this);
        reader.readAsBinaryString(file[0]);
    };
    CadastroComponent.prototype.manipularReader = function (readerEvt) {
        var binaryString = readerEvt.target.result;
        this.imageBase64 = btoa(binaryString);
    };
    CadastroComponent = __decorate([
        Component({
            selector: 'app-cadastro',
            templateUrl: './cadastro.component.html'
        }),
        __metadata("design:paramtypes", [FormBuilder,
            Router,
            ProdutoService])
    ], CadastroComponent);
    return CadastroComponent;
}());
export { CadastroComponent };
//# sourceMappingURL=cadastro.component.js.map