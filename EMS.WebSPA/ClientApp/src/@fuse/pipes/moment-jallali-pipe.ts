import { Pipe, PipeTransform } from '@angular/core';
import * as jmoment from 'jalali-moment';

@Pipe({
  name: 'momentJalaali'
})
export class MomentJalaaliPipe implements PipeTransform {
  transform(value: any, args?: any): any {
    if (value === '' || value === null || typeof (value) === undefined) { return ''; }
    const MomentDate = jmoment(new Date(value));
    return MomentDate.locale('fa').format('YYYY/MM/DD HH:mm:ss');

  }
}
