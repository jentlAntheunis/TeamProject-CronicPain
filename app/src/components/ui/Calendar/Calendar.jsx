import * as React from "react";
import { DayPicker } from "react-day-picker";
import clsx from "clsx";
import styles from "./Calendar.module.css";
import { CaretLeft, CaretRight } from "@phosphor-icons/react";

function Calendar({ className, classNames, showOutsideDays = true, ...props }) {
  return (
    <DayPicker
      showOutsideDays={showOutsideDays}
      className={clsx(styles.dayPicker, className)}
      classNames={{
        months: styles.months,
        month: styles.month,
        caption: styles.caption,
        caption_label: styles.captionLabel,
        nav: styles.nav,
        nav_button: clsx(styles.navButton, "btn-reset"),
        nav_button_previous: styles.navButtonPrevious,
        nav_button_next: styles.navButtonNext,
        table: styles.table,
        head_row: styles.headRow,
        head_cell: styles.headCell,
        row: styles.row,
        cell: styles.cell,
        day: clsx(styles.day, "btn-reset"),
        day_range_end: "day-range-end",
        day_selected: styles.daySelected,
        day_today: styles.dayToday,
        day_outside: clsx("day-outside", styles.dayOutside),
        day_disabled: styles.dayDisabled,
        day_range_middle: styles.dayRangeMiddle,
        day_hidden: styles.dayHidden,
        ...classNames,
      }}
      components={{
        IconLeft: ({ ...props }) => <CaretLeft size={16} />,

        IconRight: ({ ...props }) => <CaretRight size={16} />,
      }}
      {...props}
    />
  );
}
Calendar.displayName = "Calendar";

export { Calendar };
