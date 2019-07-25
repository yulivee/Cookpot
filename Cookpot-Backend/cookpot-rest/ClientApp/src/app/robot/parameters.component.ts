import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { RobotParameters } from './RobotParameters';
import { RobotService } from './robot.service';

@Component({
  selector: 'app-parameters',
  templateUrl: './parameters.component.html',
  styleUrls: ['./drive.component.css']
})


export class RobotParametersComponent {

  constructor(private _robotService: RobotService) {
  }
  
  public params : RobotParameters = new RobotParameters();

  public setParams(): void {
    console.log(this.params);

    this._robotService.setRobotParameters(this.params);
  }

  public setMotorState(state: boolean): void {
    this._robotService.setMotorState(state);
  }

  public zeroAxis(): void {
    this._robotService.zeroAxis();
  }
}