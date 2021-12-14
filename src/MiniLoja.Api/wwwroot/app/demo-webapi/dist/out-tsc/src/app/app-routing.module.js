import { __decorate } from "tslib";
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { CadastroComponent } from './produtos/cadastro/cadastro.component';
import { ListaComponent } from './produtos/lista/lista.component';
var routes = [
    { path: 'cadastro-produtos', component: CadastroComponent },
    { path: 'lista-produtos', component: ListaComponent }
];
var AppRoutingModule = /** @class */ (function () {
    function AppRoutingModule() {
    }
    AppRoutingModule = __decorate([
        NgModule({
            imports: [RouterModule.forRoot(routes)],
            exports: [RouterModule]
        })
    ], AppRoutingModule);
    return AppRoutingModule;
}());
export { AppRoutingModule };
//# sourceMappingURL=app-routing.module.js.map