import { Component, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { RobotJoints } from './RobotJoints';

export interface RobotCustomAction {
    code : number;
    params: string;
}

export interface RobotReply {
  stderr : string;
  stdout : string;
}

@Component({
  selector: 'app-cust-cmd',
  templateUrl: './commands.component.html',
  styleUrls: ['./drive.component.css']
})
export class CustomCommandComponent {

  constructor(private _http: HttpClient, @Inject('BASE_URL') private _baseUrl: string) {
  }

  public action : RobotCustomAction = <RobotCustomAction>{
    code : 10,
    params : '0,0,0,0,0,0,0\n'
  };

  public reply : RobotReply = <RobotReply>{
    stderr : 'Communication Error - Robot not connected\n',
    stdout : 'Robot disconnected\n'
  }

  public runCmd(): void {
    console.log(this.action);

    this._http.post<RobotReply>(this._baseUrl + 'api/RobotJoint/customCmd', this.action).subscribe(result => {
      console.log('result of http post!', result);
    }, error => console.error(error));
  }
}