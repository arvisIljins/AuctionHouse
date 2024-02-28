import { create } from "zustand";

const initialState = {
  pageNumber: 1,
  pageSize: 12,
  pageCount: 1,
  searchTerm: "",
  searchValue: "",
};

export const useParamsStore = create((set) => ({
  ...initialState,
  setParams: (newParams) => {
    set((state) => {
      if (newParams.pageNumber) {
        return { ...state, pageNumber: newParams.pageNumber };
      } else {
        return { ...state, ...newParams, pageNumber: 1 };
      }
    });
  },
  reset: () => set(initialState),
  setSearch: (value) => set({ searchValue: value }),
}));
