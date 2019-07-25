export class RobotJoints {
  public joint1: number = 0;
  public joint2: number = 0;
  public joint3: number = 0;
  public joint4: number = 0;
  public joint5: number = 0;
  public joint6: number = 0;
  public gripper: number = 0;

/*  public toString() : string {
     return Object.keys(this).filter(propertyName => typeof(this[propertyName]) !== 'function').map(propertyName => this[propertyName]).reduce((p, c) => p.concat(',', c));
     // return joint1+','+..
  }*/
}
