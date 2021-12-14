import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { CadastroComponent } from './produtos/cadastro/cadastro.component';
import { ListaComponent } from './produtos/lista/lista.component';

const routes: Routes = [
  {path: 'cadastro-produtos', component: CadastroComponent },
  {path: 'lista-produtos', component: ListaComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
