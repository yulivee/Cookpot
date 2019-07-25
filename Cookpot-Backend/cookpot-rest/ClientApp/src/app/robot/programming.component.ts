import { Component, Inject } from '@angular/core';
import { RobotJoints } from './RobotJoints';
import { RobotService } from './robot.service';

@Component({
  selector: 'app-program',
  templateUrl: './programming.component.html',
  styleUrls: ['./drive.component.css']
})

export class ProgrammingComponent {

  constructor(private _robotService: RobotService) {

  }

  public program : string;

  public robot: RobotJoints = new RobotJoints();

  public teachCurrentPos(): void {
    this._robotService.getCurrentPosition().subscribe( robotJoints => { this.program += '10,'+ robotJoints.toString() + '\n'})
  }

  public runProgram(): void {
      this._robotService.runProgram(this.program);
  }

  public pauseProgram(): void {
    this._robotService.setMotorState(false);
  }

  public stopProgram(): void {
    this._robotService.setMotorState(false);
  }
}