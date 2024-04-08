import { createWithEqualityFn } from "zustand/traditional";

const initialState = {
  pageNumber: 1,
  pageSize: 12,
  pageCount: 1,
  searchTerm: "",
  searchValue: "",
  orderBy: "createdAt",
  filterBy: "",
  seller: undefined,
  winner: undefined,
};

export const useParamsStore = createWithEqualityFn((set) => ({
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
