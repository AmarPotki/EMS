import { MatDateFormats } from '@angular/material';
import { DateFormatterParams, CalendarDateFormatterInterface, DateAdapter } from 'angular-calendar';
import * as moment from 'jalali-moment';
import { formatDate } from '@angular/common';
import {Injectable} from '@angular/core';
export const JALALI_MOMENT_FORMATS: MatDateFormats = {
  parse: {
    dateInput: 'jYYYY/jMM/jDD'
  },
  display: {
    dateInput: 'jYYYY/jMM/jDD',
    monthYearLabel: 'jYYYY jMMMM',
    dateA11yLabel: 'jYYYY/jMM/jDD',
    monthYearA11yLabel: 'jYYYY jMMMM'
  }
};

export const MOMENT_FORMATS: MatDateFormats = {
  parse: {
    dateInput: 'l'
  },
  display: {
    dateInput: 'l',
    monthYearLabel: 'MMM YYYY',
    dateA11yLabel: 'LL',
    monthYearA11yLabel: 'MMMM YYYY'
  }
};
@Injectable()
export class CalendarJalaliDateFormatter
  implements CalendarDateFormatterInterface {
  constructor(
    protected dateAdapter: DateAdapter
  ) {
      // moment.loadPersian();
    // moment.locale('fa');
    // moment().format(); // it would be in jalali system
    // moment().add(1, 'm').format();
  //  moment.bindCalendarSystemAndLocale();

  }

  /**
   * The month view header week day labels
   */
  public monthViewColumnHeader({ date, locale }: DateFormatterParams): string {
   return formatDate(date, 'EEEE', locale);
  }

  /**
   * The month view cell day number
   */
  public monthViewDayNumber({ date, locale }: DateFormatterParams): string {
    return moment(date)
      .locale(locale)
      .format('jD');
  }

  /**
   * The month view title
   */
  public monthViewTitle({ date, locale }: DateFormatterParams): string {
    return moment(date).locale(locale).format('jMMMM') + moment(date).format('jYYYY');
  }

  /**
   * The week view header week day labels
   */
  public weekViewColumnHeader({ date, locale }: DateFormatterParams): string {
    return formatDate(date, 'EEEE', locale);
  }

  /**
   * The week view sub header day and month labels
   */
  public weekViewColumnSubHeader({
    date,
    locale
  }: DateFormatterParams): string {
    return moment(date).locale(locale).format('jMMMM') + moment(date)
      .format('jD');
  }

  /**
   * The week view title
   */
  public weekViewTitle({
    date,
    locale,
    weekStartsOn,
    excludeDays,
    daysInWeek
  }: DateFormatterParams): string {

    return formatDate(date, 'EEEE', locale) +  moment(date)
      .format('jD') + ' ' +  moment(date).locale(locale).format('jMMMM') + ' '  + moment(date).format('jYYYY');
  }

  /**
   * The time formatting down the left hand side of the week view
   */
  public weekViewHour({ date, locale }: DateFormatterParams): string {
    return formatDate(date, 'h a', locale);
  }

  /**
   * The time formatting down the left hand side of the day view
   */
  public dayViewHour({ date, locale }: DateFormatterParams): string {
    return formatDate(date, 'h a', locale);
  }

  /**
   * The day view title
   */
  public dayViewTitle({ date, locale }: DateFormatterParams): string {
    return formatDate(date, 'EEEE', locale) +  moment(date)
      .format('jD') + ' ' +  moment(date).locale(locale).format('jMMMM') + ' '  + moment(date).format('jYYYY');
  }
}
