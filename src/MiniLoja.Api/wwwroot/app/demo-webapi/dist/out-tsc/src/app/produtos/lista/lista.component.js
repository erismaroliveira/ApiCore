import { __decorate, __metadata } from "tslib";
import { Component } from '@angular/core';
import { ProdutoService } from '../services/produtoService';
var ListaComponent = /** @class */ (function () {
    function ListaComponent(produtoService) {
        this.produtoService = produtoService;
    }
    ListaComponent.prototype.ngOnInit = function () {
        var _this = this;
        this.produtoService.obterTodos()
            .subscribe(function (produtos) { return _this.produtos = produtos; }, function (error) { return _this.errorMessage = error; });
    };
    ListaComponent = __decorate([
        Component({
            selector: 'app-lista',
            templateUrl: './lista.component.html'
        }),
        __metadata("design:paramtypes", [ProdutoService])
    ], ListaComponent);
    return ListaComponent;
}());
export { ListaComponent };
//# sourceMappingURL=lista.component.js.map