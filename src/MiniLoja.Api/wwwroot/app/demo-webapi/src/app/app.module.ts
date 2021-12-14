import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { CadastroComponent } from './produtos/cadastro/cadastro.component';
import { ProdutoService } from './produtos/services/produtoService';
import { ListaComponent } from './produtos/lista/lista.component';

@NgModule({
  declarations: [
    AppComponent,
    CadastroComponent,
    ListaComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule
  ],
  providers: [
    ProdutoService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
