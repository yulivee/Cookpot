import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { AbsoluteDriveComponent } from './robot/abs-drive.component';
import { RelativeDriveComponent } from './robot/rel-drive.component';
import { RobotParametersComponent } from './robot/parameters.component';
import { CustomCommandComponent } from './robot/commands.component';
import { ProgrammingComponent } from './robot/programming.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { MatTabsModule, MatFormFieldModule, MatInputModule, MatSlideToggleModule, MatButtonModule, MatCardModule, MatGridListModule } from '@angular/material';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    RobotParametersComponent,
    CustomCommandComponent,
    ProgrammingComponent,
    AbsoluteDriveComponent,
    RelativeDriveComponent
  ],
  exports: [
    MatTabsModule,
    MatFormFieldModule,
    MatInputModule,
    MatSlideToggleModule,
    MatButtonModule,
    MatCardModule,
    MatGridListModule,
  ],
  imports: [
    FormsModule,
    ReactiveFormsModule,
    BrowserAnimationsModule,
    MatInputModule,
    MatTabsModule,
    MatFormFieldModule,
    MatSlideToggleModule,
    MatCardModule,
    MatButtonModule,
    MatGridListModule,
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'counter', component: CounterComponent },
    ])
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
