import { Component } from '@angular/core';
import { JwtdecoderService } from 'src/app/service/jwtdecoder.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent {
  constructor(private jwtservice: JwtdecoderService) {

    console.log(jwtservice.getTokenInfo());
  }

}
