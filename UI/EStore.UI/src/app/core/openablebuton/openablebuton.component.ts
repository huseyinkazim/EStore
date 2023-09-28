import {  Component, Input } from '@angular/core';

@Component({
  selector: 'app-openablebuton',
  templateUrl: './openablebuton.component.html',
  styleUrls: ['./openablebuton.component.css']
})
export class OpenablebutonComponent {
  @Input() datas: any[] = [];


}
