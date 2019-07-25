import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { RobotJoints } from './RobotJoints';
import { RobotService } from './robot.service';

@Component({
  selector: 'app-abs-drive',
  templateUrl: './abs-drive.component.html',
  styleUrls: ['./drive.component.css']
})
export class AbsoluteDriveComponent {

  constructor(private _robotService: RobotService) { }

  public robot: RobotJoints = new RobotJoints();

  public setPosition(): void {
    console.log(this.robot);

    this._robotService.absoluteMove(this.robot);
  }
}