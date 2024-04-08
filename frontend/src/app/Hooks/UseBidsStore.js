import { find } from "lodash";
import { createWithEqualityFn } from "zustand/traditional";

const initialState = {
  bids: [],
};

export const useBidsStore = createWithEqualityFn((set) => ({
  ...initialState,
  setBids: (bids) => {
    set(() => ({
      bids,
    }));
  },
  addBid: (bid) => {
    set((state) => ({
      bids: !state.bids.find((x) => x.id === bid.id)
        ? [bid, ...state.bids]
        : [...state.bids],
    }));
  },
}));
