import { useEffect } from "react";

const useTitle = (title) => {
  useEffect(() => {
    document.title = `${title} | Pebbles`;
  }, [title]);
};

export default useTitle;
